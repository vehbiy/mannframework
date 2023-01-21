var app = require('http').createServer(handler);
var io = require('socket.io')(app);
var fs = require('fs');
var users = [], connections = [];
function handler(req, res) {
    // res.writeHead(302, {
    // 'Location': 'http://localhost'
    // });
    res.end('Welcome to Garcia');
}
app.listen(3000);

// var util = require('util');
// var log_file = fs.createWriteStream(__dirname + '/debug.log', {flags : 'a'});
// var log_stdout = process.stdout;

// console.log = function(d) { //
// log_file.write(d + '\n');
// log_stdout.write(d + '\n');
// };

console.log(getDateString());

var userSockets = [];
io.on('connection', function (socket) {
    if (socket.handshake.query.apikey != "1755ADDA-7CE2-4C9D-BF83-207296D71A83") {
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
        // console.log(socket);
        socket.on('test', function (data) {
            console.log("test " + data.from + " " + data.To);
            emitUser("test", data.to, { from: data.from });
        });
    }

    // socket.on('disconnect', function () {
    // if (socket.user_id === undefined) {
    // return
    // }
    // for (var key in userSockets[socket.user_id]) {
    // if (sockets[key] === socket) {
    // userSockets[socket.user_id].splice(key, userSockets[socket.user_id]);
    // }
    // }

    // console.log(socket.user_id + " disconnected");
    // });
    // socket.emit('handshake');
    // socket.on('handshake response', function (data) {
    // socket.user_id = data.id;
    // if (userSockets[data.id] === undefined) {
    // userSockets[data.id] = [];
    // }
    // userSockets[data.id].push(socket);
    // // console.log("access_token: " + data.access_token + " id: " + data.id + "(" + getDateString() + ")");
    // console.log("id: " + data.id + "(" + getDateString() + ")");
    // });
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
