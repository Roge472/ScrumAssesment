import React, { Component } from 'react';
import { withRouter } from "react-router-dom";

import { connect } from "react-redux";

import { actionCreators } from '../../../../store/Auth/Auth';
import { bindActionCreators } from 'redux';


class MailLogin extends Component {
    state = {
        mail: '',
        password: ''
    };

    updateMail(evt) {
        this.setState({
            mail: evt.target.value
        });
    }
    updatePassword(evt) {
        this.setState({
            password: evt.target.value
        });
    }

    login = (e) => {
        e.preventDefault();
        this.props.emailLogin(this.state.mail, this.state.password);
    }

    render() {


        return (
            <div>
                <form onSubmit={(e) => this.login(e)}>
                    <div className="form-group">
                        <input value={this.state.mail} onChange={evt => this.updateMail(evt)} className="form-control" placeholder="Email or login" type="email" />
                    </div>
                    <div className="form-group">
                        <input value={this.state.password} onChange={evt => this.updatePassword(evt)} className="form-control" placeholder="******" type="password" />
                    </div>
                    <div className="row">
                        <div className="col-md-6">
                            <div className="form-group">
                                <button type="submit" className="btn btn-primary btn-block"> Login  </button>
                            </div>
                        </div>
                        <div className="col-md-6 text-right">
                            <a className="small" href="#">Forgot password?</a>
                        </div>
                    </div>
                </form>
            </div>
        );
    }
};


////const mapDispatchToProps = (dispatch) => bindActionCreators(actionCreators, dispatch)=> {
//const mapDispatchToProps = dispatch => (actionCreators, dispatch) => {
//    return {
//        login: (token) => {
//            dispatch(actionCreators.login(token));
//        }
//    }
//};

export default withRouter(connect(
    state => state.auth,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(MailLogin));
