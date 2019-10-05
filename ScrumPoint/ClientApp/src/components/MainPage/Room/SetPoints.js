import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../../../store/Room/PointRoom';

class SetPoints extends Component {
    constructor(props) {
        super(props);

        this.state = {
            points: ''
        }
    }
    handleChange = (event) => {
        this.setState({ points: event.target.value });
    }

    handleSubmit = (event) => {
        event.preventDefault();
        this.props.sendPick(this.state.points);
    }
    showPoints = (e) => {
        e.preventDefault();
        this.props.showPoints();
    }
    refreshPoints = (e) => {
        e.preventDefault();
        this.props.refreshPoints();
    }
    render() {
        return (
            <div>
                <form className="align-text-center" onSubmit={this.handleSubmit}>
                    <div className="form-group row">
                        <div className="input-group input-group-sm mb-3 small-input-style">
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="roomName">Points</span>
                            </div>
                            <input type="number" className="form-control" aria-label="Small" value={this.state.points} onChange={this.handleChange} />
                            <input className="input-group-text btn btn-secondary my-btn" type="submit" value="Submit" />
                            <input className="input-group-text btn btn-success my-btn-show" type="button" value="show" onClick={this.showPoints} />
                            <input className="input-group-text btn btn-success my-btn-show" type="button" value="refresh" onClick={this.refreshPoints} />
                        </div>
                    </div>                    
                </form>
            </div>
        );
    }
}

export default connect(
    state => state.pointRoom,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(SetPoints);
