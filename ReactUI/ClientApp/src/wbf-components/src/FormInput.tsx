import React from 'react';
import { Col, Input, Label, Row } from 'reactstrap';

interface FormInput {
    label?: string,
    isMandatory?: boolean,
    onChange?: (event: any) => void,
    isPassword?: boolean,
    additionalClassname?: string,
    value?: string,
    placeholder?: string,
    isInvalid?: boolean,
    removeLabel?: boolean,
}

enum InputType {
    button = "button",
    checkbox = "checkbox",
    color = "color",
    date = "date",
    datetimelocal = "datetime-local",
    email = "email",
    file = "file",
    hidden = "hidden",
    image = "image",
    month = "month",
    number = "number",
    password = "password",
    radio = "radio",
    range = "range",
    reset = "reset",
    search = "search",
    submit = "submit",
    tel = "tel",
    text = "text",
    time = "time",
    url = "url",
    week = "week"
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

    let inputType = isPassword ? InputType.password : InputType.text;

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (onChange)
            onChange(event.target.value);
    };

    return (
        <Row className={`form-input align-items-center ${additionalClassname}`}>
            { !removeLabel &&
                <Col sm={4} className='mt-auto'>
                    <Label>{label ? `${isMandatory ? '*' : ''}${label}` : ''}</Label>
                </Col>
            }
            <Col sm={8}>
                <Input
                    type={inputType}
                    onChange={handleInputChange}
                    value={value}
                    placeholder={placeholder}
                    className={isInvalid ? 'is-invalid' : ''}
                >
                </Input>
            </Col>
        </Row>
    )
}

export default FormInput;