import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../../../../store/MainPage/MainPage';

class CreateRoom extends Component {
    constructor(props) {
        super(props);

        this.state = {
            roomName: ''
        }
    }
    componentDidMount() {
        // This method is called when the component is first added to the document
    }

    componentDidUpdate() {
        // This method is called when the route parameters change
    }

    ensureDataFetched() {
        //  const startDateIndex = parseInt(this.props.match.params.startDateIndex, 10) || 0;
        // this.props.requestWeatherForecasts(startDateIndex);
    }
    handleChange=(event)=> {
        this.setState({ roomName: event.target.value });
    }

    handleSubmit=(event)=> {
        event.preventDefault();
        this.props.createRoom(this.state.roomName);
    }

    render() {
        return (
            <div>
                <form className="align-text-center" onSubmit={this.handleSubmit}>
                    <div className="form-group row">
                        <h3 className="align-text-center">Create room</h3>
                    </div>
                    <div className="form-group row">
                        <div className="input-group input-group-sm mb-3 small-input-style">
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="roomName">Room Name</span>
                            </div>
                            <input type="text" className="form-control" aria-label="Small" value={this.state.roomName} onChange={this.handleChange} />
                            <input className="input-group-text btn btn-secondary my-btn" type="submit" value="Create" />
                        </div>
                    </div>
                </form>
            </div>
        );
    }
}

export default connect(
    state => state.mainPage,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(CreateRoom);
