import React from 'react';
import './User.css';

const UserPoints = props => (
    <div className="row border-solid-gray">
        {props.points=='?'?
        <div className="indicator-false">

        </div>:
        <div className="indicator-true">

        </div>}
        <div className="user-name">
            <p>{props.name}</p>
        </div>
        <div className="user-points">
            <p>{props.points}</p>
        </div>
  </div>
);

export const UserHidedPoints = props => (
    <div className="row border-solid-gray">
        {props.points == '?' ?
            <div className="indicator-false">

            </div> :
            <div className="indicator-true">

            </div>}
        <div className="user-name">
            <p>{props.name}</p>
        </div>
        <div className="user-points-hided">
        </div>
    </div>
);

export default UserPoints;
