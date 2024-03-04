import React from "react";

const CustomButton = props => {

    const { onClick, text } = props;

    return (
        <div className="button" onClick={() => onClick()}>
            { text &&
                <p className="button-text">{text}</p>
            }
        </div>
    )    

}
export default CustomButton;