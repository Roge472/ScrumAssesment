import React, { Component } from 'react';

import ProductTable from './ProductTable';


import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../../store/User/Profile';

class ProfilePage extends Component {

    componentDidMount() {
        // This method is called when the component is first added to the document
        this.ensureDataFetched();
    }

    componentDidUpdate() {
        // This method is called when the route parameters change
        this.ensureDataFetched();
    }

    ensureDataFetched() {

        this.props.requestUserProducts();
    }

    render() {
        var data = null;
        if (this.props.products) data = this.props.products;
        return (
            <div>{data ? <ProductTable products={data}/>:"empty"}</div>
        );
    }
}




export default connect(
    state => state.userProfile,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(ProfilePage);
