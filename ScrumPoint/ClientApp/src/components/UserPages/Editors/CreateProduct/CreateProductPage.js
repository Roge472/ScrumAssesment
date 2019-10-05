import React, { Component } from 'react';
import ProductEditorPage from '../../../LockedComponents/ProductEditorPage/ProductEditorPage';

import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../../../store/Products/Criteria/Criteria';
import { actionCreators as locationActionCreators } from '../../../../store/Products/Location/Location';

class CreateProductPage extends Component {
    constructor(props) {
        super(props);
        this.props.setCriteria();
        this.props.nullLocation();
    }


    render() {

        return (
            <div>
                <ProductEditorPage id={null}
                    name={null}
                    image={null}
                    shortDescription={null}
                    productLocat={null}
                    description={null}
                    criteria={null}
                />
            </div>
        );

    }

}


const mapDispatchToProps = (dispatch) => {
    return {
        setCriteria: () => {
            dispatch(actionCreators.setCriteria(null));
        },
        nullLocation: () => {
            dispatch(locationActionCreators.nullLocation(null));
        }
    }
}

export default connect(
    null,
    mapDispatchToProps
)(CreateProductPage);
