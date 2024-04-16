import { Room, Client } from "colyseus";
import { Schema, Context, MapSchema, ArraySchema } from "@colyseus/schema";
const type = Context.create();

export class Vector2Float extends Schema {
    @type("uint32") id = 0;
    @type("number") x = Math.floor(Math.random() * 256) - 128;
    @type("number") z = Math.floor(Math.random() * 256) - 128;
}

export class Player extends Schema {
    @type("number") x = Math.floor(Math.random() * 256) - 128;
    @type("number") z = Math.floor(Math.random() * 256) - 128;
    @type("uint8") d = 0;
    @type("number") h = Math.floor(Math.random()* 360);
    @type("uint16") score = 0;
}

export class State extends Schema {
    @type({ map: Player }) players = new MapSchema<Player>();
    @type([Vector2Float]) apples = new ArraySchema<Vector2Float>();

    appleLastId = 0;
    
    createApple() {
        const apple = new Vector2Float();
        apple.id = this.appleLastId++;
        this.apples.push(apple);
    }

    collectApple(player: Player, data: any) {
        const apple = this.apples.find((value) => value.id === data.id);
        if (apple === undefined) return;

        apple.x = Math.floor(Math.random() * 256) - 128;
        apple.z = Math.floor(Math.random() * 256) - 128;

        player.score++;
        player.d = Math.round(player.score / 3);
    }

    createPlayer(sessionId: string) {
        this.players.set(sessionId, new Player());
    }

    removePlayer(sessionId: string) {
        this.players.delete(sessionId);
    }

    movePlayer (sessionId: string, movement: any) {
        this.players.get(sessionId).x = movement.x;
        this.players.get(sessionId).z = movement.z;
    }
}

export class StateHandlerRoom extends Room<State> {
    maxClients = 4;
    startAppleCount = 100;

    onCreate (options) {
        this.setState(new State());

        this.onMessage("move", (client, data) => {
           this.state.movePlayer(client.sessionId, data);
        });

        this.onMessage("collect", (client, data) => {
            const player = this.state.players.get(client.sessionId);
            this.state.collectApple(player, data);
        })

        for(let i = 0; i < this.startAppleCount; i++) {
            this.state.createApple();
        }
    }

    onAuth(client, options, req) {
        return true;
    }

    onJoin (client: Client) {
        //client.send("hello", "world");
        this.state.createPlayer(client.sessionId);
    }

    onLeave (client) {
        this.state.removePlayer(client.sessionId);
    }

    onDispose () {
        console.log("Dispose StateHandlerRoom");
    }

}
