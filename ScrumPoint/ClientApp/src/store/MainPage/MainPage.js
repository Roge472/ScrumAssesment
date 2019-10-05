import { push } from 'react-router-redux';

const createRoomType = 'CREATE_ROOM';

const initialState = { chatRoomGuid:'' };


export const actionCreators = {
    createRoom: (chatRoomName) => async (dispatch, getState) => {
        const response = await fetch(`api/Main/AddChatRoom?roomName=${chatRoomName}`, {
            method: 'POST'
        });
        const chatRoomGuid = await response.json();
        if (chatRoomGuid) {
            dispatch(push(`/room/${chatRoomGuid}`));
        }
        dispatch({ type: createRoomType, chatRoomGuid: chatRoomGuid });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    switch (action.type) {
        case createRoomType:
            state = { ...state, chatRoomGuid: action.chatRoomGuid };
            break;
        default:
            break;
    };

    return state;
};
