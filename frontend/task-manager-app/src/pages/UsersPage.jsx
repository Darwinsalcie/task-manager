import React, { useState, useEffect } from 'react';
import { userService } from '../services/userService';
import UserCard from '../components/UserCard';

const UsersPage = () => {
  const [users, setUsers] = useState([]);
  const [errors, setErrors] = useState([]);

  const fetchUsers = async () => {
    try {
      const data = await userService.getAll();
      setUsers(data);
    } catch (err) {
      setErrors(['Error al cargar usuarios']);
    }
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  const handleDeleteUser = async (id) => {
    if (window.confirm('¿Seguro que deseas eliminar este usuario?')) {
      try {
        await userService.delete(id);
        setUsers(prev => prev.filter(u => u.id !== id));
      } catch (err) {
        alert('Error al eliminar usuario');
      }
    }
  };

  return (
    <div className="page-container">
      <h1 className="page-title">Gestión de Usuarios</h1>

      {errors.length > 0 && (
        <div className="error-message">
          {errors.map((error, idx) => (
            <div key={idx}>{error}</div>
          ))}
        </div>
      )}

      {users.length === 0 ? (
        <p style={{ color: 'var(--text-muted)' }}>No hay usuarios registrados.</p>
      ) : (
        <div className="cards-grid" style={{ gridTemplateColumns: 'repeat(auto-fill, minmax(280px, 1fr))' }}>
          {users.map(user => (
            <UserCard
              key={user.id}
              user={user}
              onDelete={handleDeleteUser}
            />
          ))}
        </div>
      )}
    </div>
  );
};

export default UsersPage;
