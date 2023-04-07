const fs = require('fs');

export default function handler(req, res) {
  if (req.method === "POST") {
    const leaderboard = require("../../../../leaderboard.json");
    updateUser(leaderboard, req);
    fs.writeFileSync('leaderboard.json', JSON.stringify(leaderboard));
    res.status(201);
  } else if (req.method === "GET") {
    const leaderboard = require("../../../../leaderboard.json");
    res.status(200).json(leaderboard);
  } else {
    req.status(404);
  }
}

function updateUser(leaderboard, req) {
  for (let user of leaderboard.scores) {
    if (user["sub"] === req.body.sub) {
      user["score"] = req.body.score;
      user["username"] = req.body.username;
      return;
    }  
  }
  leaderboard.scores.push({
    "sub":req.body.sub,
    "score":req.body.score,
    "username": req.body.username
  });
}
