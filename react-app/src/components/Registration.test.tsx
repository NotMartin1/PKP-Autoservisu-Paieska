import 'jest-localstorage-mock';
import '@testing-library/jest-dom';

import { BrowserRouter as Router } from 'react-router-dom';

import { render, fireEvent, waitFor } from '@testing-library/react';
import { Provider } from 'react-redux';
import { configureStore } from '@reduxjs/toolkit';

import Registration from './Registration';
import authReducer from '../store/slices/authSlice';

const renderWithRedux = (
  component,
  { store = configureStore({ reducer: { auth: authReducer },}) } = {}
) => {
  try {
    return {
      ...render(<Provider store={store}>
        <Router>{component}</Router></Provider>),
      store,
    };
  } catch (error) {
    console.error('Error during render:', error);
    throw error; 
  }
};

test('renders Registration component', () => {
  const { getAllByText } = renderWithRedux(<Registration />);
  const elements = getAllByText(/Registracija/i);
  expect(elements.length).toBeGreaterThan(0);
});

test('allows inputting full name', () => {
  const { getByLabelText } = renderWithRedux(<Registration />);
  const fullNameInput = getByLabelText(/Jūsų pilnas vardas/i) as HTMLInputElement;
  fireEvent.change(fullNameInput, { target: { value: 'John Doe' } });
  expect(fullNameInput.value).toBe('John Doe');
});

test('allows inputting login', () => {
  const { getByLabelText } = renderWithRedux(<Registration />);
  const loginInput = getByLabelText(/Prisijungimo vardas/i) as HTMLInputElement;
  fireEvent.change(loginInput, { target: { value: 'johndoe' } });
  expect(loginInput.value).toBe('johndoe');
});

test('allows inputting password', () => {
  const { getByLabelText } = renderWithRedux(<Registration />);
  const passwordInput = getByLabelText(/Slaptažodis/i) as HTMLInputElement;
  fireEvent.change(passwordInput, { target: { value: 'password123' } });
  expect(passwordInput.value).toBe('password123');
});

test('submits the form', async () => {
  const { getByLabelText } = renderWithRedux(<Registration />);
  fireEvent.change(getByLabelText(/Jūsų pilnas vardas/i), { target: { value: 'John Doe' } });
  fireEvent.change(getByLabelText(/Prisijungimo vardas/i), { target: { value: 'johndoe@mail.com' } });
  fireEvent.change(getByLabelText(/Slaptažodis/i), { target: { value: 'password123' } });
  fireEvent.change(getByLabelText(/Slaptažodžio pakartojimas/i), { target: { value: 'password123' } });

  fireEvent.click(getByLabelText(/Registruotis/i));

  expect(getByLabelText('Kraunama...')).toBeInTheDocument();
});

test('shows error on blur, when passwords are not the same', async () => {
  const { getByLabelText, getByText } = renderWithRedux(<Registration />);
  fireEvent.change(getByLabelText(/Slaptažodis/i), { target: { value: 'password' } });
  fireEvent.change(getByLabelText(/Slaptažodžio pakartojimas/i), { target: { value: 'password123' } });

  fireEvent.blur(getByLabelText(/Slaptažodžio pakartojimas/i));

  expect(getByText('Nesutampa slaptažodžiai!')).toBeInTheDocument();
});

test('disables button on submit', async () => {
  const { getByLabelText } = renderWithRedux(<Registration />);
  fireEvent.change(getByLabelText(/Jūsų pilnas vardas/i), { target: { value: 'John Doe' } });
  fireEvent.change(getByLabelText(/Prisijungimo vardas/i), { target: { value: 'johndoe@mail.com' } });
  fireEvent.change(getByLabelText(/Slaptažodis/i), { target: { value: 'password123' } });
  fireEvent.change(getByLabelText(/Slaptažodžio pakartojimas/i), { target: { value: 'password123' } });

  fireEvent.click(getByLabelText(/Registruotis/i));

  expect(getByLabelText('Kraunama...')).toHaveClass('btn-disabled');
});

test('shows error message when full name is missing', () => {
  const { getByLabelText, getByText } = renderWithRedux(<Registration />);
  fireEvent.click(getByLabelText(/Registruotis/i));
  expect(getByText('Neužpildyti privalomi laukeliai!')).toBeInTheDocument();
});