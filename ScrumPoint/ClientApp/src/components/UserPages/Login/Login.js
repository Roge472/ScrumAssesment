import React, { Component } from 'react';
import { connect } from "react-redux";

import { actionCreators } from '../../../store/Auth/Auth';
import { bindActionCreators } from 'redux';

import config from '../../../config.json';
import { withRouter, Redirect } from "react-router-dom";

import MailLogin from './MailLogin/MailLogin';
import GoogleLogin from './SocialLogin/GoogleLogin';

class Login extends Component {

    redirectToSignUpForm = () => {
        this.props.history.push('signup');
    }

    render() {
        let content = !!this.props.auth.isAuthenticated ?
            (
                <div>
                    <Redirect to={{
                        pathname: '/'
                    }} />
                </div>
            ) :
            (
                <div>

                    <div className="container">
                        <div className="row">
                            <div className="card" style={{ width: '50%', margin: 'auto' }}>
                                <article className="card-body">
                                    <a onClick={() => this.redirectToSignUpForm()} className="float-right btn btn-outline-primary">Sign up</a>
                                    <h4 className="card-title mb-4 mt-1">Sign in</h4>
                                    <p>
                                        <button className="btn btn-block btn-outline-info"> <i className="fab fa-twitter"></i> Login via Twitter</button>
                                        <button className="btn btn-block btn-outline-primary"> <i className="fab fa-facebook-f"></i> Login via facebook</button>
                                        <GoogleLogin/>
                                    </p>
                                    <hr />
                                    <MailLogin/>
                                </article>
                            </div>
                        </div>
                    </div>

                </div>
            );

        return (
            <div>
                {content}
            </div>
        );
    }
};

const mapStateToProps = (state) => {
    return {
        auth: state.auth
    };
};

////const mapDispatchToProps = (dispatch) => bindActionCreators(actionCreators, dispatch)=> {
//const mapDispatchToProps = dispatch => (actionCreators, dispatch) => {
//    return {
//        login: (token) => {
//            dispatch(actionCreators.login(token));
//        }
//    }
//};

export default withRouter(connect(mapStateToProps, dispatch => bindActionCreators(actionCreators, dispatch))(Login));
