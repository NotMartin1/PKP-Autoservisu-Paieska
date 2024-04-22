import { useState, useEffect } from 'react';
import {Row, Col} from 'reactstrap';
import { useHistory } from 'react-router';
import { Link } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { toast } from 'react-toastify';

import { ApiService } from 'services/ApiService';

import Button from 'shared-components/src/Button';
import CenteredForm from 'shared-components/src/CenteredForm';
import FormInput from 'shared-components/src/FormInput';

import { Path } from './constants/StaticPaths';
import { Endpoint } from './constants/Endpoints';
import { RootState } from '../store/store';


const Registration = () => {
  const [isProcessing, setIsProcessing] = useState(false);

  const [fullNameState, setFullNameState] = useState({ value: null, invalid: false });
  const [loginState, setLoginState] = useState({ value: null, invalid: false });
  const [passwordState, setPasswordState] = useState({ value: null, invalid: false });
  const [repeatPasswordState, setRepeatPasswordState] = useState({ value: null, invalid: false });

  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const history = useHistory();
  const apiService = ApiService();
  const isLoggedIn = useSelector((state: RootState) => state.auth.isLoggedIn);

  const changeRoute = (returnBack: boolean, path?: Path, ) => {
    if(returnBack && history.length > 1) {
      history.goBack();
    }

    history.replace(path || Path.Home);
  };

  useEffect(() => {
    if (isLoggedIn){
      changeRoute(true);
    }
  });


  const handleRegistration = () => {
    const Data = { Username: loginState.value, Password: passwordState.value, AdditionalData: {
      Fullname: fullNameState.value,
    } };

    setIsProcessing(true);

    apiService.post(Endpoint.Register, Data)
      .then(data => {
        if (data.Success){
          changeRoute(false, Path.Login);
          toast.success('Jūsų paskyra sėkmingai sukurta!', {
            position: 'top-center',
            autoClose: 3000,
            hideProgressBar: true,
            closeOnClick: true,
            pauseOnHover: true,
            theme: 'light',
          });
        }
        else {
          setErrorMessage(data.Message || 'Technical Error Occurred');
          setPasswordState({ value: null, invalid: false });
        }
      })
      .catch(() => toast.error('Įvyko techninė klaida!', {
        position: 'top-center',
        autoClose: 3000,
        hideProgressBar: true,
        closeOnClick: true,
        pauseOnHover: true,
        theme: 'light',
      })).finally(() => setIsProcessing(false));
  };

  const onRegister = () => {
    if (!validateInput())
      return;

    handleRegistration();
  };

  const validateInput = () => {
    setErrorMessage(null);

    const missingFields: string[] = [];

    if (!fullNameState.value) {
      missingFields.push('jūsų pilnas vardas');
      setFullNameState((prevState) => ({...prevState, invalid: true}));
    }

    if (!loginState.value) {
      missingFields.push('prisijungimo vardas');
      setLoginState((prevState) => ({...prevState, invalid: true}));
    }

    if (!passwordState.value) {
      missingFields.push('slaptažodis');
      setPasswordState((prevState) => ({...prevState, invalid: true}));
    }

    if (!repeatPasswordState.value) {
      missingFields.push('slaptažodžio pakartojimas');
      setRepeatPasswordState((prevState) => ({...prevState, invalid: true}));
    }

    if (missingFields.length > 0) {
      setErrorMessage('Neužpildyti privalomi laukeliai!');
      return false;
    }

    return true;
  };

  return (
    <>
      <CenteredForm>
        <h1 className='center mb-4'>Registracija</h1>
        <FormInput
          placeholder='Įveskite vardą bei pavardę'
          onChange={(e) => { setFullNameState(({value: e, invalid: false})); setErrorMessage(null);}}
          value={fullNameState.value}
          isInvalid={fullNameState.invalid}
          label='Jūsų pilnas vardas'
          additionalClassname='mt-3'
        />
        <FormInput
          placeholder='Įveskite prisijungimo vardą'
          onChange={(e) => { setLoginState(({value: e, invalid: false})); setErrorMessage(null); }}
          value={loginState.value}
          isInvalid={loginState.invalid}
          label='Prisijungimo vardas'
          additionalClassname='mt-3'
        />
        <FormInput
          isPassword
          placeholder='Įveskite slaptažodį'
          onChange={(e) => { setPasswordState(({value: e, invalid: false})); setErrorMessage(null);}
          }
          value={passwordState.value}
          isInvalid={passwordState.invalid}
          label='Slaptažodis'
          additionalClassname='mt-3'
        />
        <FormInput
          isPassword
          placeholder='Įveskite slaptažodį pakartotinai'
          onChange={(e) => { setRepeatPasswordState(({value: e, invalid: false})); setErrorMessage(null);}
          }
          onBlur={() => setErrorMessage(repeatPasswordState.value === passwordState.value ? null : 'Nesutampa slaptažodžiai!')}
          value={repeatPasswordState.value}
          isInvalid={repeatPasswordState.invalid}
          label='Slaptažodžio pakartojimas'
          additionalClassname='mt-3'
        />
        <Row >
          <Col>
            <span className='center mt-3' style={{ whiteSpace: 'pre-wrap' }}>
              {'Turite autoservisą? '}
              <Link to={Path.WorkshopRegistration}>Registracija verslui.</Link>
            </span>
          </Col>
        </Row>
        <Row >
          <Col>
            <span className='center mt-1' style={{ whiteSpace: 'pre-wrap' }}>
              {'Jau turite paskyrą? '}
              <Link to={Path.Login}>Prisijungti.</Link>
            </span>
          </Col>
        </Row>  
        <Row className='mt-2'>
          <Col className='center' style={{ color: 'red', fontSize: '16px'}}>
            {errorMessage}
          </Col>
        </Row>
        <Row>
          <Col className='center mt-3'>
            <Button
              label={isProcessing ? 'Kraunama...': 'Registruotis'}
              onClick={() => onRegister()}
              disabled={fullNameState.invalid || loginState.invalid || passwordState.invalid || repeatPasswordState.invalid || isProcessing}
            >
            </Button>
          </Col>
        </Row>
      </CenteredForm >
    </>
  );
};

export default Registration;