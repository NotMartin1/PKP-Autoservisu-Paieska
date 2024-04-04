import { createSlice } from '@reduxjs/toolkit';

export interface AuthSlice {
  token: string | null,
  name: string | undefined,
  email: string | undefined,
  isLoggedIn: boolean,
}

const storedToken = localStorage.getItem('token') || null;

const initialState = {
  token: storedToken,
  name: undefined,
  email: undefined,
  isLoggedIn: storedToken ? true : false,
};

export const authSlice = createSlice({
  name: 'template',
  initialState,
  reducers: {
    setToken(state, action) {
      state.token = action.payload;
      console.log(action.payload);
    },
    setLogin(state) {
      if (state.token) {
        state.isLoggedIn = true;
        localStorage.setItem('token', state.token);
      }
    },
    setLogout: (state) => {
      state.token = null;
      state.isLoggedIn = false;
      localStorage.removeItem('token');
    },
  },
});

export const { setToken, setLogin, setLogout } = authSlice.actions;

export default authSlice.reducer;