import React from 'react';
import { connect } from "react-redux";
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, Row } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../store/Auth/Auth';

// import "../Styles/Texts.scss";

class NavMenu extends React.Component {
    constructor(props) {
        super(props);

        this.toggle = this.toggle.bind(this);
        this.state = {
            isOpen: false
        };
        this.props.isUserLogin();
    }

    renderUserComponents = () => {
        if (this.props.auth.isAuthenticated) {
            return [
                <NavItem>
                    <NavLink tag={Link} to="/join"><p className="text-base-color">JoinRoom</p></NavLink>
                </NavItem>,
                <NavItem key="gotoroom">
                    <NavLink tag={Link} to="/rooms"><p className="text-base-color">Rooms</p></NavLink>
                </NavItem>,
                <NavItem key="logout">
                    <NavLink tag={Link} to="/logout"><p className="text-base-color">Logout</p></NavLink>
                </NavItem>,
            ];
        } else {
            return [
                <NavItem>
                    <NavLink tag={Link} to="/login"><p className="text-base-color">Login</p></NavLink>
                </NavItem>,
                <NavItem>
                    <NavLink tag={Link} to="/join"><p className="text-base-color">JoinRoom</p></NavLink>
                </NavItem>
            ];
        }
    }

    renderModeratorComponents = () => {
        if (this.props.auth.role == "Moderator") {
            return (
                <NavItem>
                    <NavLink tag={Link} to="/moderatorProductTable"><p className="text-base-color">Moderator</p></NavLink>
                </NavItem>
            );
        }
    }

    toggle() {
        this.setState({
            isOpen: !this.state.isOpen
        });
    }
    render() {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light >
                    <Container>
                        <NavbarBrand tag={Link} to="/"><p className="text-base-color">Point</p></NavbarBrand>
                        <NavbarToggler onClick={this.toggle} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={this.state.isOpen} navbar>
                            <ul className="navbar-nav flex-grow">
                                <Row>

                                    {this.renderUserComponents()}
                                    {this.renderModeratorComponents()}
                                </Row>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}

const mapStateToProps = (state) => {
    return {
        auth: state.auth
    };
};
export default connect(mapStateToProps, dispatch => bindActionCreators(actionCreators, dispatch))(NavMenu);