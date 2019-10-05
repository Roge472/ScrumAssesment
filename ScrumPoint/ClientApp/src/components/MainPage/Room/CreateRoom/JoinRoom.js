import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

class JoinRoom extends Component {
    constructor(props) {
        super(props);

        this.state = {
            roomName: ''
        }
    }

    handleChange=(event)=> {
        this.setState({ roomName: event.target.value });
    }

    handleSubmit=(event)=> {
        event.preventDefault();
        this.setState({ redirect: true });
    }

    render() {
        return (
            <div>
                <form className="align-text-center" onSubmit={this.handleSubmit}>
                    <div className="form-group row">
                        <h3 className="align-text-center">Join room</h3>
                    </div>
                    <div className="form-group row">
                        <div className="input-group input-group-sm mb-3 small-input-style">
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="roomName">Room Guid</span>
                            </div>
                            <input type="text" className="form-control" aria-label="Small" value={this.state.roomName} onChange={this.handleChange} />
                            <input className="input-group-text btn btn-secondary my-btn" type="submit" value="Go" />
                        </div>
                    </div>
                </form>
                {this.renderRedirect()}
            </div>
        );
    }
    renderRedirect = () => {
        if (this.state.redirect) {
            return <Redirect to={'/room/' + this.state.roomName} />
        }
    }
}

export default JoinRoom;
