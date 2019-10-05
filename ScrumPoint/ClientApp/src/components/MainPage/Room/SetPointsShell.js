import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../../../store/Room/Room';
import SetPoints from './SetPoints';

class SetPointsShell extends Component {
    render() {
        return (
            <div>
                {this.props.isObserver?null:<SetPoints/>}
            </div>
        );
    }
}

export default connect(
    state => state.room,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(SetPointsShell);
