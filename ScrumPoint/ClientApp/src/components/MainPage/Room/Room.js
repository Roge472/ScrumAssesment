import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../../../store/Room/Room';
import PointRoom from './PointRoom';
import LoginToRoom from './LoginToRoom';

class Room extends Component {
    constructor(props) {
        super(props);

        this.state = {
            roomName: '',
            isLogin:false
        }
    }
    componentDidMount() {
    }

    componentDidUpdate() {
        // This method is called when the route parameters change
    }

    ensureDataFetched() {
        //  const startDateIndex = parseInt(this.props.match.params.startDateIndex, 10) || 0;
        // this.props.requestWeatherForecasts(startDateIndex);
    }
    handleChange = (event) => {
    }

    render() {
        var renderComponent = this.props.isLogin ? <PointRoom /> : <LoginToRoom/>
        return (
            <div>
                {renderComponent}
            </div>
        );
    }
}

export default connect(
    state => state.room,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Room);
