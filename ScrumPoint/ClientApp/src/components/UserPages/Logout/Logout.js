import React, { Component } from 'react';
import { connect } from "react-redux";
import { withRouter, Redirect } from "react-router-dom";
import { bindActionCreators } from 'redux';
import { actionCreators } from '../../../store/Auth/Auth';

class Logout extends Component {
    componentDidMount() {
        //this.props.logout();
    }

    render() {
        this.props.logout();
        return(
            <div><Redirect to={{
                pathname: '/'
            }} /></div>
        );
    }
};

const mapStateToProps = (state) => {
    return {
      auth: state.auth
    };
  };
  
//const mapDispatchToProps = dispatch => bindActionCreators(actionCreators,dispatch) => {
//    return {
//        logout: () => {
//            dispatch(actionCreators.logout());
//      }
//    }
//  };
  
export default withRouter(connect(mapStateToProps, dispatch => bindActionCreators(actionCreators, dispatch))(Logout));