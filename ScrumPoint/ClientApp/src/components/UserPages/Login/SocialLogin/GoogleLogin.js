import React, { Component } from 'react';
import { GoogleLogin } from 'react-google-login';
import { connect } from "react-redux";

import { actionCreators } from '../../../../store/Auth/Auth';
import { bindActionCreators } from 'redux';

import config from '../../../../config.json';
import { withRouter } from "react-router-dom";


class GoogleLoginAuth extends Component {

    onFailure = (error) => {
        alert(error);
    };

    googleResponse = (response) => {
        if (!response.tokenId) {
            console.error("Unable to get tokenId from Google", response)
            return;
        }

        const tokenBlob = new Blob([JSON.stringify({ tokenId: response.tokenId }, null, 2)], { type: 'application/json' });
        const options = {
            method: 'POST',
            body: tokenBlob,
            mode: 'cors',
            cache: 'default'
        };

        this.props.googleLogin(options);
    };

    render() {
        return (
            <GoogleLogin
                clientId={config.GOOGLE_CLIENT_ID}
                render={renderProps => (
                    <button onClick={renderProps.onClick} disabled={renderProps.disabled} className="btn btn-block btn-outline-danger"> <i className="fab fa-google-plus "></i> Login via Google plus</button>
                )}
                buttonText="Google Login"
                onSuccess={this.googleResponse}
                onFailure={this.googleResponse}
            />
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

export default withRouter(connect(mapStateToProps, dispatch => bindActionCreators(actionCreators, dispatch))(GoogleLoginAuth));
