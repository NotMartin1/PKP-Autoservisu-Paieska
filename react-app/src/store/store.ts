import { configureStore } from '@reduxjs/toolkit';

import authReducer from './slices/authSlice';
import { AuthSlice } from './slices/authSlice';

export interface RootState {
  auth: AuthSlice
}

export const store = configureStore({
  reducer: {
    auth: authReducer,
  },
});