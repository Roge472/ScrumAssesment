import * as room from './Room';
const signalR = require("@aspnet/signalr");

const setData = "SET_DATA";
const setPickers = "SET_PICKERS";
const arePointsVisibleSetTrue = "ARE_POINTS_VISIBLE_SET_TRUE";
const arePointsVisibleSetFalse = "ARE_POINTS_VISIBLE_SET_FALSE";
const isLoginFalse = "IS_LOGIN_FALSE";

const initialState = { userName: '', connectionId: '', roomName: '', isObserver: true, isLogin: false, users:[], pickers:[], forceUpdate:5, arePointsVisible:false };

var url;
var guid;
var connection;

export const actionCreators = {
    connect: () => async (dispatch, getState) => {
        url = window.location.href;
        guid = url.split("/")[url.split('/').length - 1];

        connection = new signalR.HubConnectionBuilder()
            .withUrl("/pointpoker")
            .build();
        connection.start()
            .then(() => {

                connection.invoke("Connect", guid, getState().room.userName, getState().room.isObserver);
                connection.invoke("GetData", guid);

                connection.on("SendData", data => {
                    dispatch({ type: setData, data });
                });
                connection.on("ShowPoints", datat => {
                    dispatch({ type: arePointsVisibleSetTrue})
                });
                connection.on("HidePoints", datat => {
                    dispatch({ type: arePointsVisibleSetFalse })
                });
                connection.on("SetPoint", data => {

                    var pickers = getState().pointRoom.pickers;
                    for (var i = 0; i < pickers.length; i++) {
                        if (pickers[i].connectionId == data.item1) { pickers[i].points = data.item2; break; }
                    }
                    console.log('on setPoint');
                    dispatch({ type: setPickers, pickers });
                });


            });
     
     

    },
    loadData: (data) => async (dispatch, getState) => {
        dispatch({ type: setData, data });
    },
    sendPick: (points) => async (dispatch, getState) => {
        connection.invoke("SetPoint", guid, points);
    },
    showPoints: () => async (dispatch, getState) => {
        connection.invoke("ShowPoints", guid);
    },
    refreshPoints: () => async (dispatch, getState) => {
        connection.invoke("RefreshPoints", guid);
    },
    disconnect: () => async (dispatch, getState) => {
        connection.invoke("Disconnect");

        dispatch(room.actionCreators.updateIsLogin(false));       
        dispatch({ type: isLoginFalse});
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    switch (action.type) {
        case setData:
            state = { ...state, roomName: action.data.item1, users:action.data.item2, pickers:action.data.item3 };
            break;
        case setPickers:
            state = { ...state, pickers: action.pickers, forceUpdate: Math.random() };
            break;
        case arePointsVisibleSetTrue:
            state = { ...state, arePointsVisible:true };
            break;
        case arePointsVisibleSetFalse:
            state = { ...state, arePointsVisible: false };
            break;
        case isLoginFalse:
            state = { ...state, isLogin: false };
            break;
        default:
            break;
    };

    return state;
};
