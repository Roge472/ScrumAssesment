import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../../../store/Room/PointRoom';
import UserPoints, { UserHidedPoints } from './UserPoints';
import UserObserver from './UserObserver';
import SetPoints from './SetPoints';

class Analysis extends Component {
    constructor(props) {
        super(props);

        this.state = {
            roomName: ''
        }
    }
    handleData = () => {
        var pickers = this.props.pickers;
        console.log('pickers');
        console.log(pickers);
        //{ number: null, quatity: null }
        let template = [];
        for (var i = 0; i < pickers.length; i++) {
            var j = 0;
            for (; j < template.length; j++) {
                if (pickers[i].points == template[j].number) {
                    template[j].quatity++;
                    break;
                }
            }
            if (j == template.length) {
                if (pickers[i].points != null)
                    template.push({ number: pickers[i].points, quatity: 1 });
                continue;
            }
            
        }
        return template;

    }
    getAverage = (data) => {
        let sum = 0;
        let number = 0;
        for (var i = 0; i < data.length; i++) {
            if (data[i].number == '?') continue;
            sum += data[i].number;
            number += data[i].quatity;
        }
        if (number == 0) return;
        return sum / number;
    }
    render() {
        let data = this.handleData();
        console.log('data');
        console.log(data);
        let average = this.getAverage(data);
        let renderData = [];
        renderData.push(<div>Average: {average}</div>);
        for (var i = 0; i < data.length; i++) {
            renderData.push(<div>Number: {data[i].number}- quantity: {data[i].quatity}</div>)
        }
        return (
            <div className="analysis-position">
                <div className="align-text-center">
                    {renderData}
                </div>
            </div>
        );
    }
}

export default connect(
    state => state.pointRoom,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Analysis);
