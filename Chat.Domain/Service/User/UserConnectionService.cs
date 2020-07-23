using Chat.Domain.Contracts;
using Chat.Domain.Contracts.Repository;
using Chat.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Domain.Service.User
{
    #region interface

    public interface IUserConnectionService
    {
        Guid SaveUserConnection(Guid userId, string connectionId);

        void RemoveConnection(Guid id);

        void RemoveConnectionForUser(Guid userId);

        UserConnection GetConnectionForUser(Guid userId);

        void UpdateConnection(Guid userId, string newConnection);
    }

    #endregion

    #region implementation

    public class UserConnectionService : IUserConnectionService
    {
        #region privates

        private readonly IUserConnectionRepository _userConnectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        public UserConnectionService(
            IUserConnectionRepository userConnectionRepository,
            IUnitOfWork unitOfWork)
        {
            _userConnectionRepository = userConnectionRepository;
            _unitOfWork = unitOfWork;
        }

        public UserConnection GetConnectionForUser(Guid userId)
        {
            var conn = _userConnectionRepository.GetByCondition(x => x.UserId == userId);
            return conn;
        }

        public void RemoveConnection(Guid id)
        {
            var conn = _userConnectionRepository.GetByCondition(x => x.Id == id);
            if (conn == null)
                return;

            _userConnectionRepository.Remove(conn);
            _unitOfWork.Commit();
        }

        public void RemoveConnectionForUser(Guid userId)
        {
            var conn = _userConnectionRepository.GetByCondition(x => x.UserId == userId);
            if (conn == null)
                return;

            _userConnectionRepository.Remove(conn);
            _unitOfWork.Commit();
        }

        public Guid SaveUserConnection(Guid userId, string connectionId)
        {
            var conn = _userConnectionRepository.GetByCondition(x => x.UserId == userId);
            
            if (conn != null) // update existing
            {
                conn.ConnectionId = connectionId;
                this.UpdateConnection(conn);
                return conn.Id;
            }
            else // create new
            {

                var newConnection = new UserConnection()
                {
                    UserId = userId,
                    ConnectionId = connectionId
                };

                _userConnectionRepository.Add(newConnection);
                _unitOfWork.Commit();

                return newConnection.Id;
            }
        }

        public void UpdateConnection(Guid userId, string newConnection)
        {
            var conn = _userConnectionRepository.GetByCondition(x => x.UserId == userId);
            if (conn != null)
            {
                conn.ConnectionId = newConnection;
                this.UpdateConnection(conn);
            }
        }

        #region private methods

        private void UpdateConnection(UserConnection connection)
        {
            _userConnectionRepository.Update(connection);
            _unitOfWork.Commit();
        }

        #endregion
    }

    #endregion

}
