import { Col, Row } from 'reactstrap';
import { MoreHorizontal } from 'react-feather';

const SummaryCart = props => {

  const { label, id } = props;

  return (
    <div className="summary-card" id={id} >
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
        <p className="summary-card-data ml-2"></p>
      </Col>
      <Col>

      </Col>
    </div>
  );

};
export default SummaryCart;