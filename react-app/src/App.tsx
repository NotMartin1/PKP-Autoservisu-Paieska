import { useEffect } from 'react';
import { Route, useHistory } from 'react-router';
import { useSelector } from 'react-redux';

import { RootState } from 'store/store';

import Login from './components/Login';
import Layout from './components/Layout';
import Dashboard from './components/Dashboard';
import Registration from './components/Registration';

import { Path } from './components/constants/StaticPaths';

import './css/app.css';

const App = () => {
  const isLoggedIn = useSelector((state: RootState) => state.auth.isLoggedIn);
  const history = useHistory();

  useEffect(() => {
    if (isLoggedIn) {
      // Vėliau į main page logiškiau būtų, nes appsas public pagal idėją
      history.push(Path.Login);
    }
  }, [isLoggedIn, history]);

  return (
    <Layout>
      <Route exact path='/login' component={Login} />
      <Route exact path='/dashboard' component={Dashboard}/>
      <Route exact path='/registration' component={Registration} />
    </Layout>
  );
};

export default App;