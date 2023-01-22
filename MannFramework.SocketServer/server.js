var app = require('http').createServer(handler);
var io = require('socket.io')(app);
var fs = require('fs');
var users = [], connections = [];
function handler(req, res) {
    res.end('Welcome to MannFramework');
}
app.listen(3000);

console.log(getDateString());

var userSockets = [];
io.on('connection', function (socket) {
    if (socket.handshake.query.apikey != "1111-1111-1111-1111") {
        console.log("cannt connect");
        console.log(socket.handshake.query.id);
        socket.user_id = socket.handshake.query.id;
        if (userSockets[socket.handshake.query.id] === undefined) {
            userSockets[socket.handshake.query.id] = [];
        }
        userSockets[socket.handshake.query.id].push(socket);
        console.log("id: " + socket.handshake.query.id + "(" + getDateString() + ")");
    }
    else {
        console.log("connected");
        socket.on('test', function (data) {
            console.log("test " + data.from + " " + data.To);
            emitUser("test", data.to, { from: data.from });
        });
    }
});

function emitUser(message, to_id, data) {
    sockets = userSockets[to_id];
    for (var key in sockets) {
        sockets[key].emit(message, data);
    }
}

function getDateString() {
    var date = new Date();
    var dateString = date.getDate() + "." + date.getMonth() + "." + date.getFullYear() + " " + date.getHours() + "." + date.getMinutes() + "." + date.getSeconds();
    return dateString;
}
