const loginType = 'LOGIN';
const logoutType = 'LOGOUT';
const isUserLoginType = "IS_USER_LOGIN";

const initialState = { user: '', isAuthenticated: false, role:'' };


export const actionCreators = {
    googleLogin: (options) => async (dispatch, getState) => {
        console.log(options);
        const response = await fetch("api/Auth/GoogleLogin", options);
        const result = await response.json()

        if (result != null) {
            dispatch({ type: loginType, isAuthenticated: true, role: result.role.name });
        }
        dispatch({ type: loginType, isAuthenticated: result });
    },
    emailLogin: (email, password) => async (dispatch, getState) => {
        const response = await fetch(`api/Auth/EmailLogin?email=${email}&password=${password}`, { method: 'POST' });
        const result = await response.json()

        if (result != null) {
            dispatch({ type: loginType, isAuthenticated: true, role:result.role.name });
        }
    },
    registrateEmail: (user) => async (dispatch, getState) => {
        const response = await fetch(`api/Auth/EmailRegistration`, {
            method: 'POST', body: JSON.stringify( user ), headers: {
                'Content-Type': 'application/json',
            } });
       // const result = await response.json()

    },
    logout: () => async (dispatch, getState) => {
        const response = await fetch("api/Auth/Logout", {
            method: 'POST'
        })
        dispatch({ type: logoutType });
        window.location.reload();
    },
    isUserLogin: () => async (dispatch, getState) => {

        const response = await fetch("api/User/GetUser");

        const result = await response.json();

        if (result != null) {
            dispatch({ type: isUserLoginType, isAuthenticated: true, role:result.role.name });
        }
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    switch (action.type) {
        case loginType:
            state = { ...state, isAuthenticated: action.isAuthenticated, role: action.role  };
            break;
        case logoutType:
            state = { ...state, isAuthenticated: false, role:'' };
            break;
        case isUserLoginType:
            state = { ...state, isAuthenticated: action.isAuthenticated, user: action.user, role: action.role };
            break;
        default:
            break;
    };

    return state;
};
