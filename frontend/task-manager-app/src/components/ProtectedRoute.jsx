import React, { useContext } from 'react';
import { Navigate } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';

const ProtectedRoute = ({ children, requireAdmin }) => {
  const { user } = useContext(AuthContext);

  if (!user) {
    return <Navigate to="/login" replace />;
  }

  if (requireAdmin && user.userRole !== 0) {
    return <Navigate to="/tasks" replace />;
  }

  return children;
};

export default ProtectedRoute;
