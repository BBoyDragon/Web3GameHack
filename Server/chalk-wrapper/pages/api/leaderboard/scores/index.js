var fs = require('fs');
const leaderboard = require("../../../../leaderboard.json");

export default function handler(req, res) {
  if (req.method === "POST") {
    leaderboard.scores[req.body.sub] = {
      "score":req.body.score,
      "username": req.body.user};
    fs.writeFileSync('leaderboard.json', JSON.stringify(leaderboard));
    res.status(201);
  } else if (req.method === "GET") {
    res.status(200).json(leaderboard.scores);
  } else {
    req.status(404);
  }
}
