import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Counter from './components/Counter';
import FetchData from './components/FetchData';

import Login from "./components/UserPages/Login/Login";
import Logout from "./components/UserPages/Logout/Logout";
import SignUp from './components/UserPages/Login/MailLogin/SignUpForm';
import MainPage from './components/MainPage/MainPage';
import Room from './components/MainPage/Room/Room';
import GoToRoom from './components/MainPage/Room/CreateRoom/GoToRoom';
import JoinRoom from './components/MainPage/Room/CreateRoom/JoinRoom';

export default () => (
    <Layout>
        <Route exact path='/' component={MainPage} />
        <Route path='/counter' component={Counter} />
        <Route path='/login' component={Login} />
        <Route path='/logout' component={Logout} />
        <Route path='/signup' component={SignUp} />
        <Route path='/rooms' component={GoToRoom} />
        <Route path='/room' component={Room} />
        <Route path='/join' component={JoinRoom}/>
    </Layout>
);
