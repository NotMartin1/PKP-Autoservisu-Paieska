import React from 'react';

interface ButtonProps {
    label: string
    additionalClasses?: string
    secondary?: boolean
    disabled?: boolean
    onClick?: () => void
}

const Button: React.FC<ButtonProps> = (props) => {

    const {
        label,
        additionalClasses,
        secondary,
        disabled,
        onClick,
    } = props;

    return (
        <div className={`btn ${disabled ? 'btn-disabled' : secondary ? 'btn-secondary' : 'btn-primary'}` + (additionalClasses ?? '')} onClick={() => onClick && !disabled ? onClick() : null}>
            {label}
        </div>
    )
}
export default Button;