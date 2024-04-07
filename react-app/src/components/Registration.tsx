import {Row, Col} from 'reactstrap';

import Button from 'shared-components/src/Button';
import CenteredForm from 'shared-components/src/CenteredForm';
import FormInput from 'shared-components/src/FormInput';

const Registration = () => {
  return (
    <>
      <CenteredForm>
        <h1 className='center mb-4'>Registracija</h1>
        <FormInput
          placeholder='Įveskite vardą bei pavardę'
          label='Jūsų pilnas vardas'
          additionalClassname='mt-3'
        />
        <FormInput
          placeholder='Įveskite prisijungimo vardą'
          label='Prisijungimo vardas'
          additionalClassname='mt-3'
        />
        <FormInput
          isPassword
          placeholder='Įveskite slaptažodį'
          label='Slaptažodis'
          additionalClassname='mt-3'
        />
        <FormInput
          isPassword
          placeholder='Įveskite slaptažodį pakartotinai'
          label='Slaptažodžio pakartojimas'
          additionalClassname='mt-3'
        />
        <Row >
          <Col>
            <span className='center mt-3' style={{ whiteSpace: 'pre-wrap' }}>
              {'Turite autoservisą? '}
            </span>
          </Col>
        </Row>
        <Row >
          <Col>
            <span className='center mt-1' style={{ whiteSpace: 'pre-wrap' }}>
              {'Jau turite paskyrą? '}
            </span>
          </Col>
        </Row>  
        <Row className='mt-2'>
          <Col className='center' style={{ color: 'red', fontSize: '16px'}}>
            Klaidos pranešimas
          </Col>
        </Row>
        <Row>
          <Col className='center mt-3'>
            <Button
              label={'Registruotis'}
            >
            </Button>
          </Col>
        </Row>
      </CenteredForm >
    </>
  );
};

export default Registration;