import React, { Component } from "react";
import { HubConnectionBuilder } from "@aspnet/signalr";
import "../../../style/custom.css";
import * as types from "../../../actions/types";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import {
  Modal,
  Button,
  InputGroup,
  FormControl,
  ListGroup,
  ToggleButtonGroup,
  ToggleButton,
} from "react-bootstrap";
import { searchUsers } from "../../../actions/auth";
import { createMessage } from "../../../actions/messages";
import Moment from "react-moment";

export class Home extends Component {
  static displayName = Home.name;
  constructor(props) {
    super(props);

    this.state = {
      nick: "",
      message: "",
      messages: [],
      hubConnection: null,
      chatRooms: [],
      displayModal: false,
      searchUserEmail: "",
      customChatRoomName: "",
      selectedChatRoomId: undefined,
    };
    this._renderMessages = this._renderMessages.bind(this);
    this._renderContactList = this._renderContactList.bind(this);
    this._renderCreateChatRoomModal = this._renderCreateChatRoomModal.bind(
      this
    );
    this._renderSearchUserResult = this._renderSearchUserResult.bind(this);
  }

  static propTypes = {
    user: PropTypes.object.isRequired,
    searchUsers: PropTypes.func.isRequired,
    createMessage: PropTypes.func.isRequired,
  };

  componentDidUpdate(prevProps, prevState) {
    if (prevState.selectedChatRoomId !== this.state.selectedChatRoomId) {
      this.state.hubConnection
        .invoke("GetRoomMessages", this.state.selectedChatRoomId)
        .catch((err) => console.log(err));
    }
  }

  componentDidMount = () => {
    const { user, token } = this.props;

    const hubConnection = new HubConnectionBuilder()
      .withUrl(`${types.apiUrl}/chathub`, {
        accessTokenFactory: () => `${token}`,
      })
      .build();
    console.log(hubConnection);

    this.setState(
      { hubConnection: hubConnection, nick: user.firstName },
      () => {
        this.state.hubConnection
          .start()
          .then(() => console.log("Connection started!"))
          .catch((err) =>
            console.log("Error while establishing connection :(")
          );

        this.state.hubConnection.on("ReceiveMessage", (nick, msg) => {
          const newMsg = {
            firstName: nick,
            text: msg,
          };
          const newMessageList = this.state.messages;
          newMessageList.push(newMsg);
          console.log(newMessageList);
          this.setState({ messages: newMessageList });
        });

        this.state.hubConnection.on("ChatRoomCreated", (room) => {
          console.log(room);
          this.setState({ displayModal: false }, () => {
            this.props.createMessage({
              messageSuccess: `New Room Created by ${this.props.user.email}`,
            });

            this.state.hubConnection
              .invoke("GetChatRooms", this.props.user.id)
              .catch((err) => {
                console.log(err);
              });
          });
        });

        this.state.hubConnection.on("UpdatedChatRoomList", (chatRooms) => {
          console.log(chatRooms);
          if (chatRooms.length !== 0) {
            this.setState({ chatRooms, selectedChatRoomId: chatRooms[0].id });
          }
        });

        this.state.hubConnection.on("RoomMessagesList", (roomMessages) => {
          console.log(roomMessages);
          this.setState({ messages: roomMessages }, () => {
            this.messagesEnd.scrollIntoView({ behavior: "smooth" });
          });
        });

        this.state.hubConnection.on("NewChatMessage", (message) => {
          console.log(message);
          const { messages } = this.state;
          let newMessageList = messages;
          newMessageList.push(message);
          this.setState({ messages: newMessageList }, () => {
            this.messagesEnd.scrollIntoView({ behavior: "smooth" });
          });
        });
      }
    );
  };

  sendMessage = () => {
    const { message, selectedChatRoomId } = this.state;
    const { user } = this.props;

    const request = {
      senderUserId: user.id,
      roomId: selectedChatRoomId,
      text: message,
    };

    this.state.hubConnection
      .invoke("SendMessage", request)
      .catch((err) => console.error(err));

    this.setState({ message: "" });
  };

  createChatRoom = () => {
    const { customChatRoomName, invitedUserId } = this.state;

    const room = {
      name: customChatRoomName,
      userCreatorId: this.props.user.id,
      userInvitedId: invitedUserId,
    };
    console.log(room);
    this.state.hubConnection
      .invoke("CreateChatRoom", room)
      .catch((err) => console.error(err));
  };

  render() {
    return (
      <div className="row">
        {/* left pane */}
        <div className="col-md-4">
          <this._renderContactList />
        </div>

        {/* message box */}
        <div className="container-message col-md-8 mt-5">
          {/* list messages */}
          <div className="message mb-3 mt-3">
            <this._renderMessages />
          </div>

          {/* input message */}
          <div className="message-input mb-3 mt-3">
            <div className="input-group">
              <input
                type="text"
                className="form-control"
                placeholder="... your message"
                aria-describedby="basic-addon2"
                value={this.state.message}
                onChange={(e) => this.setState({ message: e.target.value })}
              />
              <div className="input-group-append">
                <button
                  className="btn btn-outline-secondary"
                  type="button"
                  onClick={this.sendMessage}
                >
                  Send
                </button>
              </div>
            </div>
          </div>
        </div>
        <this._renderCreateChatRoomModal />
      </div>
    );
  }

  _renderContactList() {
    const { chatRooms, selectedChatRoomId } = this.state;

    return (
      <React.Fragment>
        <div className="contact-list-header">
          <h2 className="text-center text-primary">Your Chat Rooms</h2>
          <a
            href="#"
            className="text-center ml-2 pt-2"
            onClick={() => this.setState({ displayModal: true })}
          >
            <i className="fas fa-plus-circle"></i>
          </a>
        </div>
        {chatRooms.length === 0 && <label>nothing to see here</label>}

        {chatRooms.length > 0 && (
          <div className="list-group">
            {chatRooms.map((room, index) => {
              return (
                <a
                  href={`#${room.id}`}
                  onClick={() => this.setState({ selectedChatRoomId: room.id })}
                  className={
                    room.id === selectedChatRoomId
                      ? "list-group-item active"
                      : "list-group-item"
                  }
                  key={index}
                >
                  {room.name}
                </a>
              );
            })}
          </div>
        )}
      </React.Fragment>
    );
  }

  _renderMessages() {
    const { messages } = this.state;
    const { firstName, id } = this.props.user;

    if (messages.length === 0) return null;

    return (
      <div>
        {messages.map((message, index) => {
          return (
            <div
              className={
                message.userId === id
                  ? "message-box-sent float-right text-right"
                  : "message-box-recived float-left text-left"
              }
              key={index}
            >
              <div className="card">
                <div className="card-body">{message.text}</div>
              </div>
              <small className="form-text text-muted">
                {message.user.email}
                <Moment format="MMMM Do YYYY, h:mm:ss a">
                  {message.created}
                </Moment>
              </small>
            </div>
          );
        })}
        <div
          ref={(el) => {
            this.messagesEnd = el;
          }}
          style={{ clear: "both" }}
        ></div>
      </div>
    );
  }

  _renderCreateChatRoomModal() {
    return (
      <Modal show={this.state.displayModal}>
        <Modal.Header closeButton>
          <Modal.Title>Modal heading</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <h4>Search user by email</h4>
          <InputGroup className="mb-3">
            <FormControl
              placeholder="user@email.com"
              value={this.state.searchUserEmail}
              onChange={(e) =>
                this.setState({ searchUserEmail: e.target.value })
              }
            />
            <InputGroup.Append>
              <Button
                variant="outline-secondary"
                onClick={() =>
                  this.props.searchUsers(this.state.searchUserEmail)
                }
              >
                Search
              </Button>
            </InputGroup.Append>
          </InputGroup>
          <this._renderSearchUserResult />

          {this.state.invitedUserId && (
            <div className="mt-3">
              <FormControl
                className="mb-2"
                placeholder="custom chat room name"
                value={this.state.customChatRoomName}
                onChange={(e) =>
                  this.setState({ customChatRoomName: e.target.value })
                }
              />
              <Button variant="info" onClick={this.createChatRoom}>
                Create chat room
              </Button>
            </div>
          )}
        </Modal.Body>
        <Modal.Footer>
          <Button
            variant="secondary"
            onClick={() => this.setState({ displayModal: false })}
          >
            Close
          </Button>
          <Button
            variant="primary"
            onClick={() => this.setState({ displayModal: false })}
          >
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>
    );
  }

  _renderSearchUserResult() {
    const { searchUserList } = this.props;

    if (searchUserList.length === 0) return null;

    return (
      <ToggleButtonGroup type="radio" name="options">
        {searchUserList.map((user, index) => {
          return (
            <ToggleButton
              key={index}
              value={user.id}
              className="ml-2"
              onChange={(e) =>
                this.setState({ invitedUserId: e.currentTarget.value })
              }
            >
              {user.firstName} {user.lastName}
            </ToggleButton>
          );
        })}
      </ToggleButtonGroup>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    user: state.auth.user,
    searchUserList: state.auth.searchUserList,
    token: state.auth.token,
  };
};

export default connect(mapStateToProps, { searchUsers, createMessage })(Home);
