import React, { useState } from "react";
import { Col, Row } from "reactstrap";
import { MoreHorizontal } from "react-feather";

const SummaryCart = props => {

    const { label, id } = props;

    const [optionsContextMenuOpened, setOptionsConextMenuOpened] = useState(false);
    const [summaryData, setSummaryData] = useState('$19,750');

    const handleMouseLeave = () => {
        setOptionsConextMenuOpened(false);
    };

    return (
        <div className="summary-card" id={id} onMouseLeave={handleMouseLeave}>
            <Row className="mt-2">
                <Col sm={8} className="summary-card-label">
                    {label}
                </Col>
                <Col sm={1} className="ml-3">
                    <MoreHorizontal className="summary-card-button">

                    </MoreHorizontal>
                </Col>
            </Row>
            <Col sm={3}>
                <p className="summary-card-data ml-2">{summaryData}</p>
            </Col>
            <Col>

            </Col>
        </div>
    )

}
export default SummaryCart;