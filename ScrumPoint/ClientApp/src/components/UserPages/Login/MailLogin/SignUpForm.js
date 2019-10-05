import React, { Component } from 'react';
import { withRouter, Redirect } from "react-router-dom";

import { connect } from "react-redux";

import { actionCreators } from '../../../../store/Auth/Auth';
import { bindActionCreators } from 'redux';


class MailLogin extends Component {
    state = {
        mail: '',
        firstName: '',
        lastName: '',
        password: '',
        isMessageSanded: false
    };

    updateMail(evt) {
        this.setState({
            mail: evt.target.value
        });
    }
    updateFirstName(evt) {
        this.setState({
            firstName: evt.target.value
        });
    }
    updateLastName(evt) {
        this.setState({
            lastName: evt.target.value
        });
    }
    updatePassword(evt) {
        this.setState({
            password: evt.target.value
        });
    }

    register = (e) => {
        e.preventDefault();
        var user = {
            Email: this.state.mail,
            FirstName: this.state.firstName,
            LastName: this.state.lastName,
            Password: this.state.password
        }
        this.props.registrateEmail(user);
        this.setState({ isMessageSanded: true })
    }

    renderMainContent = () => {
        let renderData = this.state.isMessageSanded ?
            (
                <div className="alert alert-success" role="alert">
                    Sended message to email
                </div>
            ) : (
                <div className="container">
                    <div className="row">
                        <div className="card" style={{ width: '50%', margin: 'auto' }}>
                            <article className="card-body">
                                <form onSubmit={(e) => this.register(e)}>
                                    <div className="form-group">
                                        <input onChange={evt => this.updateFirstName(evt)} className="form-control" placeholder="First name" type="text" />
                                    </div>
                                    <div className="form-group">
                                        <input onChange={evt => this.updateLastName(evt)} className="form-control" placeholder="Last name" type="text" />
                                    </div>
                                    <div className="form-group">
                                        <input onChange={evt => this.updateMail(evt)} className="form-control" placeholder="Email or login" type="email" />
                                    </div>
                                    <div className="form-group">
                                        <input onChange={evt => this.updatePassword(evt)} className="form-control" placeholder="******" type="password" />
                                    </div>
                                    <div className="row">
                                        <div className="col-md-6">
                                            <div className="form-group">
                                                <button type="submit" className="btn btn-primary btn-block"> Register  </button>
                                            </div>
                                        </div>
                                    </div>
                                </form>
                            </article>
                        </div>
                    </div>
                </div>
            );
        return renderData;
    }

    render() {
        let renderedData = this.props.isAuthenticated ?
            (
                <div>
                    <Redirect to={{
                        pathname: '/'
                    }} />
                </div>
            ) :
            (
                this.renderMainContent()
            );
        return (
            <div>
                {renderedData}
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
