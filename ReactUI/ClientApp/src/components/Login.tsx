//@ts-nocheck
import { useState } from 'react';
import { Alert, Col, Row } from 'reactstrap';
import { useHistory } from 'react-router-dom/cjs/react-router-dom.min';

import { Path } from './constants/StaticPaths';

import helpersRequest from '../functions/helpersRequest';
import Button from '../shared-components/src/Button';
import CenteredForm from '../shared-components/src/CenteredForm';
import FormInput from '../shared-components/src/FormInput';

const Login = props => {

  const [loginState, setLoginState] = useState({ value: null, invalid: false });
  const [passwordState, setPasswordState] = useState({ value: null, invalid: false });
  const [errorMessage, setErrorMessage] = useState(null);

  const history = useHistory();

  const changeRoute = () => {
    history.push(Path.Users);
  };

  const processLogin = () => {
    helpersRequest.post('user/login', { Login: loginState.value, Password: passwordState.value })
      .then(data => {
        if (data.success)
          changeRoute();
        else {
          setErrorMessage(data.errorMessage || 'Technical Error Occurred');
          setPasswordState({ value: null, invalid: false });
        }
      })
      .catch(error => setErrorMessage('Technical error occurred'));
  };

  const onLogin = () => {
    if (!validateInput())
      return;

    processLogin();
  };

  const validateInput = () => {
    setErrorMessage(null);

    const missingFields = [];

    if (!loginState.value) {
      missingFields.push('Login');
      setLoginState((prevState) => ({...prevState, invalid: true}));
    }

    if (!passwordState.value) {
      missingFields.push(missingFields.length == 0 ? 'Password' : 'password');
      setPasswordState((prevState) => ({...prevState, invalid: true}));
    }

    if (missingFields.length > 0) {
      setErrorMessage(`${missingFields.join(', ')} ${missingFields.length === 1 ? 'is' : 'are'} missing`);
      return false;
    }

    return true;
  };

  return (
    <>
      <CenteredForm>
        <FormInput
          placeholder='Login'
          onChange={(e) => { setLoginState(({value: e, invalid: false})); setErrorMessage(passwordState.invalid ? 'Password is missing' : null); }}
          value={loginState.value}
          isInvalid={loginState.invalid}
          removeLabel
          additionalClassname='justify-content-center'
        />
        <FormInput
          isPassword
          placeholder='Password'
          onChange={(e) => { setPasswordState(({value: e, invalid: false})); setErrorMessage(loginState.invalid ? 'Login is missing' : null); }}
          isInvalid={passwordState.invalid}
          additionalClassname='justify-content-center'
          removeLabel
        />
        <Row className='mt-3 justify-content-center'>
          <Col sm={5} style={{ color: 'red', fontSize: '16px' }}>
            {errorMessage}
          </Col>
          <Col sm={3} className='pl-5'>
            <Button
              label='Login'
              onClick={() => onLogin()}
              disabled={loginState.invalid || passwordState.invalid}
            >
            </Button>
          </Col>
        </Row>5
      </CenteredForm >
    </>
  );

};
export default Login;