import React, { Component } from 'react';
import { Route } from 'react-router';
import './css/app.css'

import Login from './components/Login';
import Layout from './components/Layout';
import Dashboard from './components/Dashboard';
import UsersList from './components/UsersList';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Login} />
        <Route exact path='/dashboard' component={Dashboard}/>
        <Route exact path='/users' component={UsersList}/>
      </Layout>
    );
  }
}
