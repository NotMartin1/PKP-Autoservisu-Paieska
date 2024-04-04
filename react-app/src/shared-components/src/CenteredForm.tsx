interface CenteredFormProps {
    children?: any,
}

const CenteredForm: React.FC<CenteredFormProps> = (props) => {

  const {
    children
  } = props;

  return (
    <div className="centered-form-container">
      <form className="centered-form">
        <div className='centered-form-content'>
          {children}
        </div>
      </form>
    </div>
  );
};

export default CenteredForm;