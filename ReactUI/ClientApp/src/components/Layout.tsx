import { Container } from 'reactstrap';
import { useLocation } from 'react-router-dom/cjs/react-router-dom.min';

import Sidebar from './Sidebar';
import Header from './Header';

const Layout = props => {

  const { children } = props;
  const location = useLocation();
  const isLoginPage = location.pathname === '/';

  return (
    <>
      { !isLoginPage && <Sidebar/> }
      <Container>
        { !isLoginPage && <Header/>}
        {children}
      </Container>
    </>
  );

};
export default Layout;