
$data = {Invoke-WebRequest -Uri https://localhost:44311/api/GitHubTest/AllRoutes -UseBasicParsing}


for ($i=1; $i -le 100000; $i++) {(Start-Job -ScriptBlock $data),"`n"}