import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';

// Function to get the access token from cookies
const getAccessToken = () => {
  return localStorage.getItem('accessToken');
}

// Function to check if the user is authenticated
const isAuthenticated = () => {
  return !!getAccessToken();
}

const ProtectedRoute = () => {
  if (!isAuthenticated()) {
    return <Navigate to="/login" replace />;
  }

  return <Outlet />;
};

export default ProtectedRoute;