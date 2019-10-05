const loadUser = "LOAD_USER";
const setUserName = "SET_USER_NAME";
const setIsObserver = "SET_IS_OBSERVER";
const setIsLogin = 'SET_IS_LOGIN';
const isUserLoginType = 'LOGIN';


const initialState = { userName: '', connectionId: '', roomName: '', isObserver: true, isLogin: false };


export const actionCreators = {
    loadUser: () => async (dispatch, getState) => {

        const response = await fetch(`api/Main/UserName`, {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        });
        console.log(response);
        const userName = await response.json();
        dispatch({ type: loadUser, userName });
    },
    setUserName: (userName) => async (dispatch, getState) => {
        dispatch({ type: setUserName, userName });
    },
    updateStatus: (isObserver) => async (dispatch, getState) => {
        dispatch({ type: setIsObserver, isObserver });
    },
    updateIsLogin: (isLogin) => async (dispatch, getState) => {
        dispatch({ type: setIsLogin, isLogin });
    },
};

export const reducer = (state, action) => {
    state = state || initialState;

    switch (action.type) {
        case loadUser:
            state = { ...state, userName: action.userName };
            break;
        case setUserName:
            state = { ...state, userName: action.userName };
            break;
        case setIsObserver:
            state = { ...state, isObserver: action.isObserver };
            break;
        case setIsLogin:
            state = { ...state, isLogin: action.isLogin };
            break;
        default:
            break;
    };

    return state;
};
