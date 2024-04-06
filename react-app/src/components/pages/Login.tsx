import { useState, useEffect } from 'react';
import { Col, Row } from 'reactstrap';
import { useHistory, Link } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { setLogin, setToken } from 'store/slices/authSlice';

import { ApiService } from 'services/ApiService';
import { RootState } from 'store/store';

import { Path } from '../constants/StaticPaths';
import { Endpoint } from '../constants/Endpoints';

import Button from '../../shared-components/src/Button';
import CenteredForm from '../../shared-components/src/CenteredForm';
import FormInput from '../../shared-components/src/FormInput';

const Login = () => {
  const [isProcessing, setIsProcessing] = useState(false);

  const [loginState, setLoginState] = useState({ value: null, invalid: false });
  const [passwordState, setPasswordState] = useState({ value: null, invalid: false });
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const history = useHistory();
  const dispatch = useDispatch();
  const apiService = ApiService();

  const changeRoute = (redirect: boolean) => {
    if(redirect && history.length > 1) {
      history.goBack();
    }
  
    history.replace(Path.Home);
  };

  const loggedIn = useSelector((state: RootState) => state.auth.isLoggedIn);

  useEffect(() => {
    if (loggedIn){
      changeRoute(true);
    }
  });

  const processLogin = () => {
    const Data = { Username: loginState.value, Password: passwordState.value };

    setIsProcessing(true);
  
    apiService.post(Endpoint.Login, Data)
      .then(data => {
        if (data.Success){
          dispatch(setToken('token'));
          dispatch(setLogin());
          changeRoute(false);
        }
        else {
          setErrorMessage(data.Message || 'Technical Error Occurred');
          setPasswordState({ value: null, invalid: false });
        }
      })
      .catch(error => setErrorMessage(`Technical error occurred: ${error}`)).finally(() => setIsProcessing(false));
  };

  const onLogin = () => {
    if (!validateInput())
      return;

    processLogin();
  };

  const validateInput = () => {
    setErrorMessage(null);

    const missingFields: string[] = [];

    if (!loginState.value) {
      missingFields.push('prisijungimo vardas');
      setLoginState((prevState) => ({...prevState, invalid: true}));
    }

    if (!passwordState.value) {
      missingFields.push('slaptažodis');
      setPasswordState((prevState) => ({...prevState, invalid: true}));
    }

    if (missingFields.length > 0) {
      setErrorMessage(`Neužpildyti laukeliai: ${missingFields.join(', ')}`);
      return false;
    }

    return true;
  };

  return (
    <>
      <CenteredForm>
        <h1 className='center mb-4'>Prisijungimas</h1>
        <FormInput
          placeholder='Įveskite prisijungimo vardą'
          onChange={(e) => { setLoginState(({value: e, invalid: false})); setErrorMessage(passwordState.invalid ? 'Password is missing' : null); }}
          value={loginState.value}
          isInvalid={loginState.invalid}
          label='Prisijungimo vardas'
          additionalClassname='mt-3'
        />
        <FormInput
          isPassword
          placeholder='Įveskite slaptažodį'
          onChange={(e) => { setPasswordState(({value: e, invalid: false})); setErrorMessage(loginState.invalid ? 'Login is missing' : null);}
          }
          value={passwordState.value}
          isInvalid={passwordState.invalid}
          label='Slaptažodis'
          additionalClassname='mt-3'
        />
        <Row >
          <Col>
            <span className='center mt-3' style={{ whiteSpace: 'pre-wrap' }}>
              {'Dar neturite paskyros? '}
              <Link to={Path.Registration}>Registruotis.</Link>
            </span>
          </Col>
        </Row>
        <Row className='mt-2'>
          <Col className='center' style={{ color: 'red', fontSize: '16px' }}>
            {errorMessage}
          </Col>
        </Row>
        <Row>
          <Col className='center mt-3'>
            <Button
              label= {isProcessing ? 'Kraunama...' : 'Prisijungti'}
              onClick={() => onLogin()}
              disabled={loginState.invalid || passwordState.invalid || isProcessing}
            >
            </Button>
          </Col>
        </Row>
      </CenteredForm >
    </>
  );

};
export default Login;