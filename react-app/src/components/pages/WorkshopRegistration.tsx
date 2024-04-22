import { useState, useEffect } from 'react';
import { Col, Row } from 'reactstrap';
import { useHistory, Link } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { toast } from 'react-toastify';

import { ApiService } from 'services/ApiService';
import { RootState } from 'store/store';

import { Path } from '../constants/StaticPaths';
import { Endpoint } from '../constants/Endpoints';

import Button from '../../shared-components/src/Button';
import CenteredForm from '../../shared-components/src/CenteredForm';
import FormInput from '../../shared-components/src/FormInput';

const WorkshopRegistration = () => {
  const [isProcessing, setIsProcessing] = useState(false);

  const [loginState, setLoginState] = useState({ value: null, invalid: false });
  const [passwordState, setPasswordState] = useState({ value: null, invalid: false });
  const [repeatPasswordState, setRepeatPasswordState] = useState({ value: null, invalid: false });
  const [companyNameState, setCompanyNameState] = useState({ value: null, invalid: false });
  const [companyAdressState, setCompanyAdressState] = useState({ value: null, invalid: false });
  const [companyPhoneState, setCompanyPhoneState] = useState({ value: null, invalid: false });
  const [companyEmailState, setCompanyEmailState] = useState({ value: null, invalid: false });

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
    const Data = { Username: loginState.value, Password: passwordState.value, AdditionalData: { CompanyName: companyNameState.value, Address: companyAdressState.value, PhoneNumber: companyPhoneState.value, Email: companyEmailState.value } };

    setIsProcessing(true);

    apiService.post(Endpoint.WorkshopRegister, Data)
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

    if (!companyNameState.value) {
      missingFields.push('įmonės pavadinimas');
      setCompanyNameState((prevState) => ({...prevState, invalid: true}));
    }

    if (!companyAdressState.value) {
      missingFields.push('įmonės adresas');
      setCompanyAdressState((prevState) => ({...prevState, invalid: true}));
    }

    if (!companyPhoneState.value) {
      missingFields.push('telefono numeris');
      setCompanyPhoneState((prevState) => ({...prevState, invalid: true}));
    }

    if (!companyEmailState.value) {
      missingFields.push('el. paštas');
      setCompanyEmailState((prevState) => ({...prevState, invalid: true}));
    }

    if (missingFields.length > 0) {
      setErrorMessage('Neužpildyti privalomi laukeliai!');
      return false;
    }

    return true;
  };

  return (
    <>
      <div className='mt-2'/>
      <CenteredForm>
        <h1 className='center mb-4'>Autoserviso Registracija</h1>
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
        <hr className='divider'/>
        <FormInput
          placeholder='Įveskite įmonės pavadinimą'
          onChange={(e) => { setCompanyNameState(({value: e, invalid: false})); setErrorMessage(null);}}
          value={companyNameState.value}
          isInvalid={companyNameState.invalid}
          label='Įmonės pavadinimas'
          additionalClassname='mt-3'
        />
        <FormInput
          placeholder='Įveskite įmonės adresą'
          onChange={(e) => { setCompanyAdressState(({value: e, invalid: false})); setErrorMessage(null);}}
          value={companyAdressState.value}
          isInvalid={companyAdressState.invalid}
          label='Įmonės adresas'
          additionalClassname='mt-3'
        />
        <FormInput
          placeholder='Įveskite įmonės telefono numerį'
          onChange={(e) => { setCompanyPhoneState(({value: e, invalid: false})); setErrorMessage(null);}}
          value={companyPhoneState.value}
          isInvalid={companyPhoneState.invalid}
          label='Telefono numeris'
          additionalClassname='mt-3'
        />
        <FormInput
          placeholder='Įveskite įmonės el.pašto adresą'
          onChange={(e) => { setCompanyEmailState(({value: e, invalid: false})); setErrorMessage(null);}}
          value={companyEmailState.value}
          isInvalid={companyEmailState.invalid}
          label='El. paštas'
          additionalClassname='mt-3'/>
        {/* <FormInput
          placeholder='Įveskite vardą bei pavardę'
          onChange={(e) => { setFullNameState(({value: e, invalid: false})); setErrorMessage(null);}}
          value={fullNameState.value}
          isInvalid={fullNameState.invalid}
          label='Internetinis adresas'
          additionalClassname='mt-3'
        />
        <FormInput
          placeholder='Įveskite vardą bei pavardę'
          onChange={(e) => { setFullNameState(({value: e, invalid: false})); setErrorMessage(null);}}
          value={fullNameState.value}
          isInvalid={fullNameState.invalid}
          label='Aprašymas'
          additionalClassname='mt-3'
        /> */}
        <Row >
          <Col>
            <span className='center mt-3' style={{ whiteSpace: 'pre-wrap' }}>
              {'Esate automobilio savininkas? '}
              <Link to={Path.Registration}>Įprasta registracija.</Link>
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
              disabled={loginState.invalid || passwordState.invalid || repeatPasswordState.invalid || companyAdressState.invalid || companyEmailState.invalid || companyNameState.invalid || companyPhoneState.invalid || isProcessing}
            >
            </Button>
          </Col>
        </Row>
      </CenteredForm >
      <div className='mb-2'/>
    </>
  );

};
export default WorkshopRegistration;