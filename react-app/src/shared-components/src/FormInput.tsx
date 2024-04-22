import {Input, Label, Row } from 'reactstrap';
import React, { FocusEvent } from 'react';

interface FormInput {
    label?: string,
    isMandatory?: boolean,
    onChange?: (value: string) => void,
    onBlur?: (event: FocusEvent<HTMLInputElement>) => void;
    isPassword?: boolean,
    additionalClassname?: string,
    value?: string | null,
    placeholder?: string,
    isInvalid?: boolean,
    removeLabel?: boolean,
}

enum InputType {
    button = 'button',
    checkbox = 'checkbox',
    color = 'color',
    date = 'date',
    datetimelocal = 'datetime-local',
    email = 'email',
    file = 'file',                  
    hidden = 'hidden',
    image = 'image',
    month = 'month',
    number = 'number',
    password = 'password',
    radio = 'radio',
    range = 'range',
    reset = 'reset',
    search = 'search',
    submit = 'submit',
    tel = 'tel',
    text = 'text',
    time = 'time',
    url = 'url',
    week = 'week'
}

const FormInput: React.FC<FormInput> = (props) => {

  const {
    label,
    isMandatory,
    onChange,
    isPassword,
    additionalClassname,
    value,
    placeholder,
    isInvalid,
    removeLabel
  } = props;

  const inputType = isPassword ? InputType.password : InputType.text;

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (onChange)
      onChange(event.target.value);
  };

  return (
    <div className={`form-input ${additionalClassname}`}>
      { !removeLabel &&
        <Row>
          <Label>{label ? `${isMandatory ? '*' : ''}${label}` : ''}</Label>
        </Row>
      }
      <div>
        <Input
          type={inputType}
          onChange={handleInputChange}
          onBlur={props.onBlur}
          aria-label={label}
          value={value || ''}
          placeholder={placeholder}
          className={isInvalid ? 'is-invalid' : ''}
        >
        </Input>
      </div>
    </div>
  );
};

export default FormInput;