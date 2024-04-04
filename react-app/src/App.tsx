import { useEffect } from 'react';
import { Route, useHistory } from 'react-router';
import { useSelector } from 'react-redux';
import { ToastContainer } from 'react-toastify';

import { RootState } from 'store/store';

import Home from 'components/pages/Home';
import NotFound from 'components/pages/NotFound';

import Login from './components/pages/Login';
import Registration from './components/pages/Registration';
import WorkshopLogin from 'components/pages/WorkshopLogin';
import WorkshopRegistration from 'components/pages/WorkshopRegistration';
import Dashboard from './components/pages/Dashboard';
import UsersList from './components/pages/UsersList';
import MainLayout from 'components/layouts/MainLayout';
import { Path } from './components/constants/StaticPaths';

import 'react-toastify/dist/ReactToastify.css';
import './css/app.css';


const App = () => {
  const isLoggedIn = useSelector((state: RootState) => state.auth.isLoggedIn);
  const history = useHistory();

  useEffect(() => {
    if (isLoggedIn) {
      history.push(Path.Home);
    }
  }, [isLoggedIn, history]);

  //      <Route path='*' component={NotFound} />

  return (
    <>
      <ToastContainer />
      <MainLayout>
        <Route exact path='/' component={Home} />
        <Route exact path='/login' component={Login} />
        <Route exact path='/registration' component={Registration} />
        <Route exact path='/workshop-login' component={WorkshopLogin} />
        <Route exact path='/workshop-registration' component={WorkshopRegistration} />
        <Route exact path='/dashboard' component={Dashboard}/>
        <Route exact path='/users' component={UsersList}/>
      </MainLayout>
    </>
  );
};

export default App;