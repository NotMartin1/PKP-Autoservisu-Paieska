import React from 'react';
import { Container } from 'reactstrap';
import Sidebar from './Sidebar';
import Header from './Header';

import { useLocation } from 'react-router-dom/cjs/react-router-dom.min';

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
  )

}
export default Layout;