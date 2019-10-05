import React, { Component } from 'react';

import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../../../store/Products/Product';

import ProductEditorPage from '../../../LockedComponents/ProductEditorPage/ProductEditorPage';

class EditProductPage extends Component {

    componentDidMount() {
        // This method is called when the component is first added to the document
        this.ensureDataFetched();
    }

    componentDidUpdate() {
        // This method is called when the route parameters change
        this.ensureDataFetched();
    }

    ensureDataFetched() {
        const produtId = window.location.search.substring(4);
        this.props.requestProductById(produtId);
    }

    render() {
        var product = this.props.product;
        if (product) {
            return (
                <div>
                    <ProductEditorPage id={product.id}
                        name={product.name}
                        image={product.mainImage}
                        shortDescription={product.shortDescription}
                        productLocat={product.productLocation}
                        description={product.description}
                        criteria={product.criteria}
                    />
                </div>
            );
        }
        return (<div>Render</div>)
    }

}




export default connect(
    state => state.product,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(EditProductPage);
