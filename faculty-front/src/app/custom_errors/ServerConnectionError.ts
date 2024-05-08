export class ServerConnectionError extends Error {
    constructor(message: string = 'Failed to connect to server.') {
        super(message);
    }
}

