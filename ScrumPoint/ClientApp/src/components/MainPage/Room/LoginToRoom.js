import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../../../store/Room/Room';

class PointRoom extends Component {

    componentDidMount() {
        this.props.loadUser();
    }

    handleChange = (event) => {
        this.props.setUserName(event.target.value);
    }

    handleSubmit = (event) => {
        event.preventDefault();
        this.props.updateIsLogin(true);
    }

    render() {

        return (
            <div>
                <div className="align-text-center">
                    <h3 className="align-text-center">Login to room</h3>
                    <form onSubmit={this.handleSubmit}>
                        <div className="input-group">
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="basic-addon1">Name:</span>
                            </div>
                            <input type="text" className="form-control" placeholder="Username" aria-label="Username" aria-describedby="basic-addon1" value={this.props.userName} onChange={this.handleChange} />
                            <div className="input-group-append">
                                <div className="dropdown">
                                    <button className="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        {this.props.isObserver?'Observer':'Picker'}
                                    </button>
                                    <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        <a className="dropdown-item" onClick={(e) => { e.preventDefault(); this.props.updateStatus(true); }}>Observer</a>
                                        <a className="dropdown-item" onClick={(e) => { e.preventDefault(); this.props.updateStatus(false); }}>Picker</a>
                                    </div>
                                </div>
                            </div>
                            <input className="input-group-text btn btn-secondary my-btn" type="submit" value="Submit" />

                        </div>
                    </form>
                </div>
            </div>
        );
    }
}

export default connect(
    state => state.room,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(PointRoom);
