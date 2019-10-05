import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../../../store/Room/PointRoom';
import UserPoints, { UserHidedPoints } from './UserPoints';
import UserObserver from './UserObserver';
import SetPointsShell from './SetPointsShell';
import Analysis from './Analysis';

class PointRoom extends Component {
    constructor(props) {
        super(props);

        this.state = {
            roomName: ''
        }
    }
    componentDidMount() {
        this.props.connect();
    }

    ensureDataFetched() {
        //  const startDateIndex = parseInt(this.props.match.params.startDateIndex, 10) || 0;
        // this.props.requestWeatherForecasts(startDateIndex);
    }
    componentWillUnmount() {
        this.props.disconnect();
    }
    displayUsers = () => {
        var users = this.props.users;
        var pickers = this.props.pickers;
        for (let i = 0; i < users.length; i++) {
            for (let j = 0; j < pickers.length; j++) {
                if (pickers[j].connectionId == users[i].connectionId) {
                    users[i].points = pickers[j].points ? pickers[j].points:'?'; break;
                }
            }
        }
        console.log(users);
        let renderData = users.map(u => {
            if (u.points) {
                if (this.props.arePointsVisible) {
                    return <UserPoints key={u.name} name={u.name} points={u.points} />
                } else {
                    return <UserHidedPoints key={u.name} name={u.name} points={u.points} />
                }
            } else {
                return <UserObserver key={u.name} name={u.name}/> 
            }
        });
        return renderData;
    }
    getAnalysis = () => {
        if (this.props.arePointsVisible) return <Analysis/>
    }
    render() {
        let renderData = this.displayUsers();
        let analysis = this.getAnalysis();
        console.log('this.props.isObserver');
        console.log(this.props.isObserver);
        return (
            <div>
                <div className="align-text-center">
                    <h3 className="align-text-center">Point Room {this.props.roomName}</h3>
                    <SetPointsShell/>
                    {renderData}
                    {analysis}
                </div>
            </div>
        );
    }
}

export default connect(
    state => state.pointRoom,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(PointRoom);
