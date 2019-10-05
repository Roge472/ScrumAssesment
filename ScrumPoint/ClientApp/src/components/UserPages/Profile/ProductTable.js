import React, { Component } from 'react';
import { withRouter } from 'react-router-dom'

import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../../store/Products/Product';


import ReactTable from 'react-table';
import "react-table/react-table.css";

import './ProductTable.scss';

class ProfilePage extends Component {

    render() {
        var data = null;
        if (this.props.products) data = this.props.products;
        return (
            <div>{data ? this.renderProduct(data) : "empty"}</div>
        );
    }

    redirectButton = (cellInfo) => {
        return (
            <button className='product-table-button product-table-edit-button ' onClick={e => {
                this.props.history.push('/editProduct?id='+cellInfo.id);
            }}> Edit</button>
        )
    }

    deleteProuct = (product) => {
        this.props.deleteProuct(product.id);
        this.props.history.push('profile');
    }

    columns = [{
        id: 'name',
        Header: 'Name',
        accessor: d => d.name// String-based value accessors!
    },
    {
        Header: 'Edit',
        id: 'edit',
        maxWidth: 150,
        accessor: this.redirectButton
    },
    {
        Header: 'Delete',
        id: 'delete',
        maxWidth: 120,
        Cell: d => {
            return <div className='product-table-button-div'>
                <button className='product-table-button product-table-delete-button' onClick={() => this.deleteProuct(d.original)}>Delete</button>
            </div>
        }
    }];

    renderProduct = (data) => {
        return (<ReactTable
            data={data}
            columns={this.columns}
            defaultPageSize={10}
            className="-striped -highlight"
        />)
    }
}




export default connect(
    state => state.product,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(withRouter(ProfilePage));
