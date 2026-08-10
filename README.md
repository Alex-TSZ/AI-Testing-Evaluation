# AI-Testing-Evaluation
A platform to test different AI models on simular or different datasets.

Define the problem<br>
This platform evaluates AI models across different tasks, subjects, and difficulty levels. It supports benchmarking, model comparison, analytics, and optional training dataset generation. Results are stored and analyzed to measure model performance across various domains and capabilities.

Identify main objects<br>
- Model Types
	- Functionalities
   		- Model

- Subject
	- topics
		- Question
			- ExpectedOutput/GroundTruth (In the code I will just name it ExpectedOutput)

- Dataset
	- Question

- Model
	- BenchmarkSession
		- Evaluation
			- Question
			- ModelResponse
			- Score
			- IsCorrect

Relationships\
Model Types 1 - M -> Functionalities 1 - M -> Model\
Subject 1 - M -> topics 1 - M -> Question 1 - 1 -> ExpectedOutput/GroundTruth\
Dataset 1 - M -> Question\
Model 1 - M -> BenchmarkSession 1 - M -> Evaluation <br>

Database Design

ModelType
---------------
PK  Id			INT\
	Name		VARCHAR(100) \
	Description	TEXT <br>
	
Functionality
---------------
PK	 Id		        INT<br>
FK	 ModelTypeId	INT<br>
     Description	TEXT<br>
	   Name		      VARCHAR(100)<br>

Architecture
---------------
PK	Id		INT<br>
	Name		VARCHAR(100)<br>
	Description	TEXT<br>

Model
---------------
PK	Id		INT<br>
FK	FunctionalityId	INT<br>
FK	ArchitectureId	INT<br>
	Name		VARCHAR(100)<br>
	Version		VARCHAR(50)<br>
	Description	TEXT<br>

Subject
---------------
PK	Id		INT<br>
	Name		VARCHAR(100)<br>
	Description	TEXT<br>

Topic
---------------
PK	Id		INT<br>
FK	SubjectId	INT<br>
	Name		VARCHAR(100)<br>
	Description	TEXT<br>


Dataset
---------------
PK	Id		INT <br>
FK	TopicId		INT<br>
	Name 		VARCHAR(100)<br>
	DataType	VARCHAR(50)<br>
	Description	TEXT<br>

Question
---------------
PK	Id			INT<br>
FK	TopicId			INT<br>
FK	DatasetId		INT<br>
	QuestionType		enum<br>
	EstimatedDifficulty	INT<br>
	TestedDifficulty	INT<br>
	Prompt 			TEXT<br>

ExpectedOutput
---------------
PK	Id		INT<br>
FK	QuestionId	INT<br>
	OutputData	JSON<br>

BenchmarkSession
---------------
PK	Id		INT<br>
FK	ModelId		INT<br>
FK	DatasetId	INT<br>
	StartTime	DATETIME<br>
	EndTime		DATETIME<br>
	Status		VARCHAR(50)<br>

EvaluationResult
---------------
PK	Id			INT<br>
FK	BenchmarkSessionId	INT<br>
FK	QuestionId		INT<br>
FK	ExpectedOutputId	INT<br>
	ModelResponse		JSON<br>
	ResponseTimeMS		INT<br>
	Score			DECIMAL(5, 2)<br>
	IsCorrect		BIT<br>
	CreatedAt		DATETIME<br>

Design APIs<br>
Run Benchmark
---------------
POST /api/benchmarksessions<br>
GET /api/benchmarksessions<br>
GET /api/benchmarksessions/{id}<br>
GET /api/benchmarksessions/{id}/results<br>
POST /api/benchmarksessions/{id}/start<br>
POST /api/benchmarksessions/{id}/finish<br>

Submit Model Response
---------------
POST /api/benchmarksessions/{id}/responses<br>

Analytics
---------------
GET /api/models/{id}/analytics/<br>

Model Comparison
---------------
GET /api/models/{id}/analytics/compare?first={id}&second={id}<br>

ModelType
---------------
POST /api/modeltypes<br>
GET /api/modeltypes<br>
GET /api/modeltypes/{id}<br>
DELETE /api/modeltypes/{id}<br>

Functionality
---------------
POST /api/functionalities<br>
GET /api/functionalities<br>
GET /api/functionalities/{id}<br>
DELETE /api/functionalities/{id}<br>

Model
---------------
POST /api/models<br>
GET /api/models<br>
GET /api/models/{id}<br>
DELETE /api/models/{id}<br>
PATCH /api/models/{id}<br>
PUT /api/models/{id}<br>

Architecture
---------------
POST /api/architectures<br>
GET /api/architectures<br>
GET /api/architectures/{id} <br>
DELETE /api/architectures/{id}<br>

Subject
---------------
POST /api/subjects<br>
GET /api/subjects<br>
GET /api/subjects/{id} <br>
DELETE /api/subjects/{id}<br>

Topic
---------------
POST /api/subjects/{id}/topics<br>
GET /api/subjects/{id}/topics<br>
GET /api/subjects/{id}/topics/{id}<br>
GET /api/subjects/{id}/topics/{id}/questions<br>
DELETE /api/subjects/{id}/topics/{id}<br>

Dataset
---------------
POST /api/topics/{id}/datasets<br>
GET /api/topics/{id}/datasets<br>
GET /api/topics/{id}/datasets/{id} <br>
DELETE /api/topics/{id}/datasets/{id}<br>

Question
---------------
POST /api/questions<br>
GET /api/questions<br>
GET /api/questions/{id} <br>
DELETE /api/questions/{id}<br>

ExpectedOutput
---------------
POST /api/questions/{id}/expectedoutputs<br>
GET /api/questions/{id}/expectedoutputs<br>
GET /api/questions/{id}/expectedoutputs/{id} <br>
DELETE /api/questions/{id}/expectedoutputs/{id}<br>

BenchmarkSession
---------------
POST /api/benchmarksessions<br>
GET /api/benchmarksessions<br>
GET /api/benchmarksessions/{id} <br>
DELETE /api/benchmarksessions/{id}<br>

EvaluationResult
---------------
POST /api/evaluationresults<br>
GET /api/evaluationresults<br>
GET /api/evaluationresults/{id} <br>
DELETE /api/evaluationresults/{id}<br>

What this project demonstrates so far:
--------------------------------------
- API design<br>
- Relational database design<br>
- Entity Framework<br>
- DTOs<br>
- Mapping<br>
- Business logic<br>
- Analytics<br>
- Working with JSON<br>

What will be added to the project:
----------------------------------
- Authentication (Low)<br>
- User accounts (Low)<br>
- Role-based permissions (Low)<br>
- API Keys (Medium)<br>
- Background benchmark execution (High)<br>
- Docker (High)<br>
- Kubernetes deployment (High)<br>
- Azure (High)<br>
- Swagger documentation (Medium)<br>
- Caching (Medium)<br>
- Logging (Medium)<br>
- Rate limiting (Low)<br>

Architecture:
-------------
Client -> frontend -> ASP.NET Core API -> controllers -> Services -> EF Core -> SQL Server<br>
													  -> Business Logic<br>
													  -> Validation<br>

Project Order:
--------------
1. Create and Finish Subject<br>
2. Create and Finish Topics<br>
3. Set up the Architecture for the models and MetaData<br>
4. Look back at the structure and relationships<br>
5. Set up Questions<br>
6. Determine Expected Output<br>
7. Creating Datasets<br>
8. Setting up Benchmark Sessions<br>
9. Setting up tables for results and saving records<br>
10. Create an evaluation engine<br>
11. Set up benchmark execution<br>
12. Analytics<br>
13. Validation and error handling<br>
14. Logging <br>
15. Testing<br>
16. Check endpoints and documentation (Swagger/OpenAI)<br>
17. Maybe Authentication<br>
18. Implement Docker<br>
19. Implement CI/CD<br>

Project Phases: (Put more detail into this section Later)
---------------
- Foundation<br>
- EF Core (Entity Framework)<br>
- SQL Servers<br>
- Migrations<br>
- Dependency Injections<br>
- Subject<br>
- Topic<br>
- Model Metadata<br>
- Learning Material<br>
- Benchmarking<br>
- Analytics<br>
- Quality<br>
- Deployment<br>
- AI Integration<br>
