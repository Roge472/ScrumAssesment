import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../../store/MainPage/MainPage';
import Description from './Description';

import "./MainPage.css"

class MainPage extends Component {
  componentDidMount() {
    // This method is called when the component is first added to the document
    this.ensureDataFetched();
  }

  componentDidUpdate() {
    // This method is called when the route parameters change
    this.ensureDataFetched();
  }

  ensureDataFetched() {
   // const startDateIndex = parseInt(this.props.match.params.startDateIndex, 10) || 0;
   // this.props.requestWeatherForecasts(startDateIndex);
  }

  render() {
    return (
        <div>
            <h1 className="align-text-center">Point</h1>
            <Description />
      </div>
    );
  }
}

export default connect(
  state => state.mainPage,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(MainPage);
