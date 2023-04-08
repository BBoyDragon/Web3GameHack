const fs = require('fs');

const leaderboardPath = "/data/www/web3-hack/ChalkMazeServer/leaderboard.json";
export default function handler(req, res) {
  if (req.method === "POST") {
    if (req.body.sub === undefined || req.body.username === undefined) {
      res.status(404).json({});
    } else {
      try {
        const leaderboard = getLeaderboard(); 
        updateUser(leaderboard, req.body.sub, req.body.username);
        fs.writeFile(leaderboardPath, JSON.stringify(leaderboard), 
          err => err ? console.error('Error writing file', err) : console.log('Successfully wrote file')
        )
        res.status(201).json({});
      } catch (err) {
        console.error(err);
      }
    }
  } else if (req.method === "GET") {
    const leaderboard = getLeaderboard();
    res.status(200).json(leaderboard);
  } else {
    res.status(404).json({});
  }
}

const getLeaderboard = () => JSON.parse(fs.readFileSync(leaderboardPath));

function updateUser(leaderboard, sub, username) {
  for (let user of leaderboard.scores) {
    if (user.sub === sub) {
      user.score++;
      user.username = username;
      return;
    }  
  }
  leaderboard.scores.push({
    "sub": sub,
    "score": 1,
    "username": username
  });
}
